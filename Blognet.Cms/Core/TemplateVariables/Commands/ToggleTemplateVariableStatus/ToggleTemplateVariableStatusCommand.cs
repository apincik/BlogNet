using MediatR;


namespace Blognet.Cms.Core.TemplateVariables.Commands.ToggleTemplateVariableStatus
{
    public class ToggleTemplateVariableStatusCommand : IRequest<Unit>
    {
        public int Id;
    }
}
