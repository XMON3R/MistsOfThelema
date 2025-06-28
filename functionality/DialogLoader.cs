using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // Potřebné pro Task
using System.Text.Json;

namespace MistsOfThelema
{
    public class DialogLoader
    {
        public Dictionary<string, Dictionary<string, DialogNode>> Dialogs { get; private set; }

        // --- ZDE JE DELEGÁT A UDÁLOST ---
        // Delegát pro oznámení o dokončení načítání dialogů
        // Obsahuje bool parametr pro signalizaci úspěchu/neúspěchu a volitelný string pro chybovou zprávu
        public delegate void DialogsLoadedEventHandler(bool success, string errorMessage = null);
        // Událost, na kterou se ostatní třídy (např. scény) mohou přihlásit
        public event DialogsLoadedEventHandler DialogsLoaded;

        public DialogLoader()
        {
            Dialogs = new Dictionary<string, Dictionary<string, DialogNode>>();
        }

        public string LoadSingleDialog(string filePath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string endPath = Path.Combine(basePath, filePath);

            string dialogContent;
            try
            {
                dialogContent = File.ReadAllText(endPath);
                return dialogContent;
            }
            catch (Exception ex) // general exception handling
            {
                dialogContent = $"{filePath} XXXXX {basePath} XXXXX {endPath} XXXXX Sorry, there seems to be a problem loading this dialog: {ex.Message}. Please contact the dev team.";
                return dialogContent;
            }
        }

        // --- UPRAVENÁ METODA PRO ASYNCHRONNÍ NAČÍTÁNÍ S UDÁLOSTÍ ---
        public async Task LoadDialogsFromJsonAsync(string filePath)
        {
            try
            {
                // Spustí načítání souboru v pozadí (na jiném vlákně z ThreadPoolu)
                string json = await Task.Run(() => File.ReadAllText(filePath));
                Dialogs = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, DialogNode>>>(json);

                // Po úspěšném načtení vyvoláme událost s informací o úspěchu
                // '?' je null-conditional operator - zajistí, že se Invoke() zavolá jen pokud někdo naslouchá
                DialogsLoaded?.Invoke(true);
            }
            catch (Exception ex)
            {
                Dialogs = null; // Nastavíme Dialogs na null v případě chyby
                // V případě chyby vyvoláme událost s informací o neúspěchu a chybovou zprávou
                DialogsLoaded?.Invoke(false, $"Failed to load dialogs from {filePath}: {ex.Message}");
            }
        }

        public DialogNode GetDialogNode(string conversationId, string nodeId)
        {
            if (Dialogs != null && Dialogs.ContainsKey(conversationId) && Dialogs[conversationId].ContainsKey(nodeId))
            {
                return Dialogs[conversationId][nodeId];
            }
            return null;
        }
    }

    public class DialogNode
    {
        public string Text { get; set; }
        public Dictionary<string, DialogChoice> Choices { get; set; }
    }

    public class DialogChoice
    {
        public string Text { get; set; }
        public string Next { get; set; }
    }
}