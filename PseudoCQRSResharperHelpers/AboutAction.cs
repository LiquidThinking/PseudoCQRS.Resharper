using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;

namespace PseudoCQRSResharperHelpers
{
	[ActionHandler("PseudoCQRSResharperHelpers.About")]
	public class AboutAction : IActionHandler
	{
		public bool Update( IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate )
		{
			// return true or false to enable/disable this action
			return true;
		}

		public void Execute( IDataContext context, DelegateExecute nextExecute )
		{
			MessageBox.Show(
			  "PseudoCQRS Reshaper Helpers\nLiquidThinkin\n\nResharper shortcuts to find CommandHandlers, ViewModelProviders and other stuff",
			  "About PseudoCQRS Reshaper Helpers",
			  MessageBoxButtons.OK,
			  MessageBoxIcon.Information );
		}
	}
}