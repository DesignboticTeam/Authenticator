using Connector.DataModels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AuthenticatorConnector.Configuration
{
    public class CustomSettingsPostConfigure : IPostConfigureOptions<CustomSettings>
    {
        private readonly IHostEnvironment _env;
        IOptions<AppSettings> _appSettings;

        public CustomSettingsPostConfigure(IHostEnvironment env, IOptions<AppSettings> appSettings)
        {
            _env = env;
            _appSettings = appSettings;
        }

        public void PostConfigure(string name, CustomSettings options)
        {
            string executionPath = _env.ContentRootPath;
            options.FullLastUserPath = Path.Combine(_appSettings.Value.TemporaryFolderPath, options.LastUserPath);
        }
    }
}


