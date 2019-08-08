using System;
using System.Linq;
using System.Reflection;
using HTTP.Enums;
using HTTP.Responses;
using MvcFramework.Attributes.Action;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using MvcFramework.Routing;

namespace MvcFramework
{
    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            AutoRegisterRoutes(application, serverRoutingTable);

            application.ConfigureServices();
            application.Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void AutoRegisterRoutes(IMvcApplication application, IServerRoutingTable serverRoutingTable)
        {
            var controllers = application.GetType().Assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract
            && typeof(Controller).IsAssignableFrom(type)); // type.IsSubclassOf(typeof(Controller))

            foreach (var controller in controllers)
            {
                /* Removed, because should implement NonActionAttribute check
                var actions = controller.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance).Where(
                    x => x.IsPublic && !x.IsStatic && !x.IsConstructor
                    && x.DeclaringType == controller && !x.IsVirtual && !x.IsSpecialName);
                    */
                var actions = controller.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                                        .Where(x => !x.IsSpecialName && x.DeclaringType == controller)
                                        .Where(x => x.GetCustomAttributes().All(a => a.GetType() != typeof(NonActionAttribute)));


                foreach (var action in actions)
                {
                    var path = $"/{controller.Name.Replace("Controller", string.Empty)}/{action.Name}";
                    var attribute = action.GetCustomAttributes()
                                          .Where(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute)))
                                          .LastOrDefault() as BaseHttpAttribute;
                    var httpMethod = HttpRequestMethod.GET;

                    if(attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if(attribute?.Url != null)
                    {
                        path = attribute.Url;
                    }

                    if(attribute?.ActionName != null)
                    {
                        path = $"/{controller.Name.Replace("Controller", string.Empty)}/{attribute.ActionName}";
                    }

                    serverRoutingTable.Add(httpMethod, path, request =>
                    {
                        var controllerInstance = Activator.CreateInstance(controller);
                        ((Controller)controllerInstance).Request = request;

                        // Security Authorization - TODO: Refactor this
                        var controllerPrincipal = ((Controller)controllerInstance).User;
                        var authorizeAttribute = action.GetCustomAttributes()
                                                       .LastOrDefault(a => a.GetType() == typeof(AuthorizeAttribute)) as AuthorizeAttribute;

                        if(authorizeAttribute != null && !authorizeAttribute.IsInAuthority(controllerPrincipal))
                        {
                            // TODO: Redirect to configured URL
                            return new HttpResponse(HttpResponseStatusCode.Forbidden);
                        }

                        var response = action.Invoke(controllerInstance, new object[0]) as ActionResult;
                        return response;
                    });

                    Console.WriteLine(httpMethod + " " + path);
                }
            }
        }
    }
}





/*
private static void AutoRegisterRoutes(IMvcApplication application, IServerRoutingTable serverRoutingTable)
{
    var controllers = application.GetType().Assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract
    && typeof(Controller).IsAssignableFrom(type)); // type.IsSubclassOf(typeof(Controller))

    foreach (var controller in controllers)
    {
        var actions = controller.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance).Where(
            x => x.IsPublic && !x.IsStatic && !x.IsConstructor
            && x.DeclaringType == controller && !x.IsVirtual && !x.IsSpecialName);

        foreach (var action in actions)
        {
            var path = $"/{controller.Name.Replace("Controller", "")}/{action.Name}";
            var attribute = action.CustomAttributes
                                   .Where(x => x.AttributeType.IsSubclassOf(typeof(BaseHttpAttribute)))
                                   .LastOrDefault();

            var httpMethod = HttpRequestMethod.GET;

            if (attribute?.AttributeType == typeof(HttpPostAttribute))
            {
                httpMethod = HttpRequestMethod.POST;
            }
            else if (attribute?.AttributeType == typeof(HttpDeleteAttribute))
            {
                httpMethod = HttpRequestMethod.DELETE;
            }

            serverRoutingTable.Add(httpMethod, path, request =>
            {
                var controllerInstance = Activator.CreateInstance(controller);
                var response = action.Invoke(controllerInstance, new[] { request }) as IHttpResponse;
                return response;
            });
        }
    }
}
*/
