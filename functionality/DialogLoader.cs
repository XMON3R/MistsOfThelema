using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // Necessary for Task-based asynchronous operations
using System.Text.Json; // Used for JSON serialization and deserialization

namespace MistsOfThelema
{
    /// <summary>
    /// Manages the loading of dialog data from JSON files into memory.
    /// </summary>
    public class DialogLoader
    {
        // A nested dictionary to store all dialogs, where the first key is the conversation ID
        // and the second key is the dialog node ID.
        public Dictionary<string, Dictionary<string, DialogNode>> Dialogs { get; private set; }

        // --- DELEGATE AND EVENT ---
        /// <summary>
        /// A delegate for notifying subscribers when dialog loading is complete.
        /// </summary>
        /// <param name="success">True if loading was successful, otherwise false.</param>
        /// <param name="errorMessage">An optional error message if loading failed.</param>
        public delegate void DialogsLoadedEventHandler(bool success, string errorMessage = null);

        public event DialogsLoadedEventHandler DialogsLoaded;

        public DialogLoader()
        {
            // Initialize the Dialogs dictionary to prevent null reference exceptions.
            Dialogs = new Dictionary<string, Dictionary<string, DialogNode>>();
        }

        /// <summary>
        /// Reads the content of a single file synchronously.
        /// </summary>
        /// <param name="filePath">The path to the dialog file, relative to the application's base directory.</param>
        /// <returns>The content of the file as a string, or an error message if the file cannot be read.</returns>
        public string LoadSingleDialog(string filePath)
        {
            string dialogContent = "";
            // Get the base directory of the application.
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            // Combine the base path with the provided file path to get the full path.
            string endPath = Path.Combine(basePath, filePath);

            try
            {
                // Read the entire file into a single string.
                dialogContent = File.ReadAllText(endPath);
                return dialogContent;
            }
            catch (Exception ex) // Catch any exception that occurs during file reading.
            {
                // Provide a detailed error message for debugging purposes.
                dialogContent = $"{filePath} XXXXX {basePath} XXXXX {endPath} XXXXX Sorry, there seems to be a problem loading this dialog: {ex.Message}. Please contact the dev team.";
                return dialogContent;
            }
        }

        // --- MODIFIED METHOD FOR ASYNCHRONOUS LOADING WITH EVENT NOTIFICATION ---
        /// <summary>
        /// Asynchronously loads dialog data from a JSON file.
        /// It notifies subscribers via the DialogsLoaded event upon completion or failure.
        /// </summary>
        /// <param name="filePath">The full path to the JSON file containing the dialog data.</param>
        public async Task LoadDialogsFromJsonAsync(string filePath)
        {
            try
            {
                // Read the file content asynchronously on a background thread from the ThreadPool
                // to avoid blocking the UI thread.
                string json = await Task.Run(() => File.ReadAllText(filePath));
                // Deserialize the JSON content into the Dialogs dictionary.
                Dialogs = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, DialogNode>>>(json);

                // Raise the DialogsLoaded event with a 'success' status.
                // The '?' is the null-conditional operator, ensuring Invoke() is called only if there are subscribers.
                DialogsLoaded?.Invoke(true);
            }
            catch (Exception ex)
            {
                // Set Dialogs to null to indicate that no data was loaded due to an error.
                Dialogs = null;
                // In case of an error, raise the event with a 'failure' status and an error message.
                DialogsLoaded?.Invoke(false, $"Failed to load dialogs from {filePath}: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific dialog node from the loaded dialogs based on conversation and node IDs.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation.</param>
        /// <param name="nodeId">The ID of the specific node within the conversation.</param>
        /// <returns>The requested DialogNode object, or null if the conversation or node is not found.</returns>
        public DialogNode GetDialogNode(string conversationId, string nodeId)
        {
            // Check if Dialogs is not null and if the specified conversation and node exist.
            if (Dialogs != null && Dialogs.ContainsKey(conversationId) && Dialogs[conversationId].ContainsKey(nodeId))
            {
                // Return the found dialog node.
                return Dialogs[conversationId][nodeId];
            }
            // Return null if the dialog node is not found.
            return null;
        }
    }

    /// <summary>
    /// Represents a single node in a dialog tree, containing text and available choices.
    /// </summary>
    public class DialogNode
    {
        // The main text of the dialog node.
        public string Text { get; set; }
        // A dictionary of choices, where the key is the choice ID and the value is a DialogChoice object.
        public Dictionary<string, DialogChoice> Choices { get; set; }
    }

    /// <summary>
    /// Represents a single choice within a dialog node.
    /// </summary>
    public class DialogChoice
    {
        // The text of the choice displayed to the player.
        public string Text { get; set; }
        // The ID of the next dialog node to transition to after this choice is selected.
        public string Next { get; set; }
    }
}