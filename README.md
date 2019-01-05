# dotNET-Core

_1.Service Configuration

// Build a customized MVC implementation, without using the default AddMvc(), instead use AddMvcCore().
    // https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs

    services
        .AddMvcCore(options =>
        {
            options.RequireHttpsPermanent = true; // does not affect api requests
            options.RespectBrowserAcceptHeader = true; // false by default
            //options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();

            //remove these two below, but added so you know where to place them...
            options.OutputFormatters.Add(new YourCustomOutputFormatter()); 
            options.InputFormatters.Add(new YourCustomInputFormatter());
        })
        //.AddApiExplorer()
        //.AddAuthorization()
        .AddFormatterMappings()
        //.AddCacheTagHelper()
        //.AddDataAnnotations()
        //.AddCors()
        .AddJsonFormatters(); // JSON, or you can build your own custom one (above)
