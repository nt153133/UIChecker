using System.Linq;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.AClasses;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.Navigation;
using ff14bot.Pathing.Service_Navigation;
using TreeSharp;
using UI_Checker;

namespace NullBotBase
{
    public class UiBotBase : AsyncBotBase
    {
        private Form1 form;
        public override string Name => "UI Tester";
        public override PulseFlags PulseFlags => PulseFlags.All;
        public override Composite Root => new ActionAlwaysFail();
        public override bool IsAutonomous => true;
        public override bool RequiresProfile => false;

        public override bool WantButton { get; } = true;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnButtonPress()
        {
            form = new Form1();

            form.Show();
        }

        public override Task AsyncRoot()
        {
            return test();
        }

        public async Task<bool> test()
        {
            Navigator.NavigationProvider = new ServiceNavigationProvider();
            Navigator.PlayerMover = new SlideMover();



            return true;
        }
        
        
    }
}