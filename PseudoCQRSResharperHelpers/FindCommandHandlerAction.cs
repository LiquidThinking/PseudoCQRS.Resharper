using System;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Feature.Services.Navigation.Navigation.NavigationExtensions;
using JetBrains.ReSharper.Intentions.Extensibility;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using JetBrains.ReSharper.Psi.Search;

namespace PseudoCQRSResharperHelpers
{
	[ContextAction(Name = "FindCommandHandler", Description = "Find Command Handler", Group = "C#")]
	public class FindCommandHandlerAction : ContextActionBase
	{
		private readonly ICSharpContextActionDataProvider _provider;
		private IDeclaration _commandType;

		public FindCommandHandlerAction( ICSharpContextActionDataProvider provider )
		{
			_provider = provider;
		}

		public override bool IsAvailable( IUserDataHolder cache )
		{
			var commandType = _provider.GetSelectedElement<IDeclaration>( true, true );
			if ( commandType != null && commandType.IsValid() )
			{
				if ( !string.IsNullOrEmpty( commandType.DeclaredName ) && commandType.DeclaredName.EndsWith( "Command" ) )
				{
					_commandType = commandType;
					return true;
				}
			}
			return false;
		}

		protected override Action<ITextControl> ExecutePsiTransaction( ISolution solution, IProgressIndicator progress )
		{
			var handlerName = _commandType.DeclaredName + "Handler.cs";
			var searchDomain = solution.GetPsiServices().SearchDomainFactory.CreateSearchDomain( solution, false );
			var references = solution.GetPsiServices().Finder.FindReferences( _commandType.DeclaredElement, searchDomain, progress );
			var handlerReference = references.FirstOrDefault( x => x.GetTreeNode().GetSourceFile().Name.EndsWith( handlerName ) );

			if ( handlerReference != null )
			{
				handlerReference.GetTreeNode().GetSourceFile().Navigate( new TextRange(), true );
			}

			return null;
		}

		public override string Text
		{
			get { return "Find Command Handler"; }
		}

	}
}
