using NotePad.Models;

public class TodoIsValidFilter : IEndpointFilter
{
    private ILogger _logger;

    public TodoIsValidFilter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TodoIsValidFilter>();
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext efiContext, 
        EndpointFilterDelegate next)
    {
        var todo = efiContext.GetArgument<ToDo>(0);

        var validationError = Utilities.IsValid(todo!);

        if (!string.IsNullOrEmpty(validationError))
        {
            _logger.LogWarning(validationError);
            return Results.Problem(validationError);
        }
        return await next(efiContext);
    }
}