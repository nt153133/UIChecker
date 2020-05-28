using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Forms;
using Buddy.Coroutines;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Helpers;
using ff14bot.Managers;
using ff14bot.RemoteWindows;

namespace UI_Checker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RaptureAtkUnitManager.Update();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            //listBox1.Items.Add($"RaptureAtkUnitManager Updated");
            LlamaUI.Log("RaptureAtkUnitManager Updated");
           

            foreach (var window in RaptureAtkUnitManager.Controls)
            {
                listBox1.Items.Add($"{window.Name}");
            }

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            //ActionManager.DoAction(11385, GameObjectManager.GetObjectByNPCId(1026934));
            richTextBox1.Text = "";
            var windowName = ((string) listBox1.SelectedItem).Trim();
            //AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(windowName);
            //string windowName = windowByName.Name;
            //ff14bot.Managers.AgentModule.AgentPointers[310]
            //listBox1.Items.Add($"{AgentModule.AgentPointers[310].ToInt64():X}");
           
            LlamaUI.Log($"{windowName} Selected");
            var elements = LlamaUI.___Elements(windowName);
            using (var outputFile = new StreamWriter($"{windowName}.cvs", false))
            {
                for (var j = 0; j < elements.Length; j++)
                {
                    var i = elements[j];
                    string data;
                    //Log(i+ " " );
                    if (i.Type == 6 || i.Type == 8 || i.Type == 38)
                        //byte[] source = Core.Memory.ReadBytes((IntPtr)i.Data, 16);
                        //byte[] bytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, source.ToArray());
                        //string @string = Encoding.Unicode.GetString(bytes);
                        data = Core.Memory.ReadString((IntPtr) i.Data, Encoding.UTF8);

                    //listBox3.Items.Add($"[{j}:{i.Type}] ({tstring})");
                    else if (i.Type == 4)
                        data = $"{i.TrimmedData}";
                    //listBox3.Items.Add($"[{j}:{i.Type}] {i.TrimmedData}");
                    else
                        data = $"{i.Data}({i.TrimmedData})";

                    listBox2.Items.Add($"[{j}:{i.Type}] {data}");
                    IntPtr ptr = (IntPtr) i.Data;
                    outputFile.WriteLine($"{j},{i.Type},{i.Data},{i.TrimmedData},{data},{ptr.ToInt64():X}");
                    LlamaUI.Log($"{windowName}.cvs Written");
                }
            }
            
            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(windowName);
            if(windowByName != null)
            {
//                Core.Memory.GetRelative()
                var test = windowByName.TryFindAgentInterface();
                richTextBox1.Text += $"Agent ID is: {test.Id}\n";
                richTextBox1.Text += $"Pointer: {test.Pointer.ToInt64():X} \nAgent Vtable: {test.VTable.ToInt64():X} \nVtableOffset {Core.Memory.GetRelative(test.VTable).ToInt64():X}\n";
                richTextBox1.Text += $"Window Pointer: {windowByName.Pointer.ToInt64():X} \nWindow Vtable: {windowByName.VTable.ToInt64():X} \nVtableOffset {Core.Memory.GetRelative(windowByName.VTable).ToInt64():X}\n";
                //richTextBox1.Text += $"Size is: {sizeof(ResultLayout)}\n";
            }

            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            var windowName = ((string) listBox1.SelectedItem).Trim();
            //AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(windowName);
            //string windowName = windowByName.Name;
            //ff14bot.Managers.AgentModule.AgentPointers[310]
            //listBox1.Items.Add($"{AgentModule.AgentPointers[310].ToInt64():X}");

            LlamaUI.Log($"{windowName} Selected");
            var elements = LlamaUI.___Elements(windowName);
            
/*            listBox3.Items.Add($"[Selected NPC 1] {LlamaUI.GetTrustNpc(elements[34].TrimmedData).Name} ({LlamaUI.GetTrustNpc(elements[34].TrimmedData).Class()}) Level: {elements[43].TrimmedData}");
            listBox3.Items.Add($"[Selected NPC 2] {LlamaUI.GetTrustNpc(elements[35].TrimmedData).Name} ({LlamaUI.GetTrustNpc(elements[35].TrimmedData).Class()}) Level: {elements[44].TrimmedData}");
            listBox3.Items.Add($"[Selected NPC 3] {LlamaUI.GetTrustNpc(elements[36].TrimmedData).Name} ({LlamaUI.GetTrustNpc(elements[36].TrimmedData).Class()}) Level: {elements[45].TrimmedData}");

            listBox3.Items.Add($"Number of trusts: {elements[73].TrimmedData}");
            listBox3.Items.Add($"Selected Trust Id: {elements[74].TrimmedData}");
            var data = Core.Memory.ReadString((IntPtr) elements[75].Data, Encoding.UTF8);
            listBox3.Items.Add($"Selected Trust Name: {data}");
            button3.Enabled = false;*/
        }

        private List<string> trusts(TwoInt[] elements)
        {
            //for (int i=)
            return new List<string>();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var test = new IshgardHandin();

            if (InventoryManager.FilledSlots.Any(i => i.RawItemId == 28757))
            {
                await test.HandInItem(28757, 0, 0);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dictionary<ff14bot.Enums.ClassJobType, int> Classes = new Dictionary<ClassJobType, int>
            {
                {ClassJobType.Carpenter,0},
                {ClassJobType.Blacksmith,1},
                {ClassJobType.Armorer,2},
                {ClassJobType.Goldsmith,3},
                {ClassJobType.Leatherworker,4},
                {ClassJobType.Weaver,5},
                {ClassJobType.Alchemist,6},
                {ClassJobType.Culinarian,7},
                {ClassJobType.Miner,8},
                {ClassJobType.Botanist,9},
                {ClassJobType.Fisher,10},
            };
            
            


/*            foreach (var job in Classes)
            {
                LlamaUI.Log($"{job.Key}:");
                MasterPieceSupply.ClassSelected = job.Value;
                Thread.Sleep(1000);
                //Coroutine.Sleep(1000);
                foreach (var item in MasterPieceSupply.GetTurninItems())
                {
                    LlamaUI.Log($"{item}");
                }
            }*/
            
            foreach (var job in Classes)
            {
                LlamaUI.Log($"{job.Key}:");
                MasterPieceSupply.ClassSelected = job.Value;
                Thread.Sleep(1000);
                foreach (var item in MasterPieceSupply.GetTurninItemsStarred())
                {
                    LlamaUI.Log($"{item.Key} Stared: {item.Value}");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            var windowName = ((string) listBox1.SelectedItem).Trim();

            if (windowName == "ItemSearch")
            {
                AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(windowName);
                var test = windowByName.TryFindAgentInterface();
                var agentPointer = test.Pointer;
                if(windowByName != null)
                {
                    int count = ItemSearch.GetCount(agentPointer);
                    richTextBox1.Text += $"Number of Results: {count}\n";

                    var items = ItemSearch.ReadResults(agentPointer, count).Where(i => i.itemPointer.ToInt64() != 0);

                    foreach (var result in items)
                    {
                        string name = Core.Memory.ReadString(result.itemPointer + 0xa0 , Encoding.UTF8);
                        listBox3.Items.Add($"{name} ({result.count})");
                    }
                }
  
            }

/*            var windowName = "RetainerSell";
            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(windowName);
            if (windowByName != null)
            {
                var agent = windowByName.TryFindAgentInterface();
                var agentPointer = agent.Pointer;
                int price = Core.Memory.Read<int>(agentPointer + 0x4b74);
                int qty = Core.Memory.Read<int>(agentPointer + 0x4b78);
                string outS = string.Format("Price {0}  Qty {1}", price, qty);
            }*/
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var results = ItemSearchResult.ReadSearchResults();

            foreach (var result in results)
            {
                listBox3.Items.Add($"Hq: {result.HQ == 1} Qty: {result.qty} Price: {result.price}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var patternFinder = new GreyMagic.PatternFinder(Core.Memory))
            {
                IntPtr SendActionBreakpoint = patternFinder.Find("48 85 C0 74 ? 48 89 18 4C 8D 70 ? 49 8B C6 48 85 DB 74 ? 89 30");
                textBox1.Text = ($"ffxiv_dx11.exe+{Core.Memory.GetRelative(SendActionBreakpoint).ToString("X")}");
            }
        }
    }
}