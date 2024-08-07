﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//install nu package
using System.Text.Json;

namespace MistsOfThelema
{
    public class DialogLoader
    {
        public Dictionary<string, Dictionary<string, DialogNode>> Dialogs { get; private set; }

        public DialogLoader()
        {
            Dialogs = new Dictionary<string, Dictionary<string, DialogNode>>();
        }

        public string LoadSingleDialog(string filePath)
        {
            string dialogContent = "";

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string endPath = Path.Combine(basePath, filePath);

            try
            {
                dialogContent = File.ReadAllText(endPath);
                return dialogContent;
            }
            catch
            {
                dialogContent = filePath + " XXXXX " + basePath + " XXXXX " + endPath + " XXXXX " + "Sorry, there seems to be a problem loading this dialog, please contact the dev team.";
                return dialogContent;
            }
        }

        public void LoadDialogFromJson(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                Dialogs = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, DialogNode>>>(json);
            }

            catch 
            {
                Dialogs = null;
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
        public string text { get; set; }
        public Dictionary<string, DialogChoice> choices { get; set; }
    }

    public class DialogChoice
    {
        public string text { get; set; }
        public string next { get; set; }
    }

}
