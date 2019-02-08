//using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CarTransports.Infrastructure
{
    public static class Utils
    {
        public static void RequireHttps(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
        }

        public static void AddSecurityHeaders(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
                {
                    context.Response.Headers.Add("X-Frame-Options", "DENY");
                    await next();
                });

            app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());

            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                .FormActions(s => s.Self())
                .FrameAncestors(s => s.Self())
            );

            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseXContentTypeOptions();

            app.UseReferrerPolicy(opts => opts.NoReferrer());

            app.Use((context, next) =>
            {
                if (context.Request.IsHttps)
                {
                    context.Response.Headers.Append("Expect-CT", $"max-age=0; report-uri=\"https://tranicars.com/report-ct\"");
                }
                return next.Invoke();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto

            });

            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseXContentTypeOptions();
        }

        public static void ErrorPages(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

        }

        public static void ClearCache(IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });
        }

        [HtmlTargetElement("div", Attributes = ValidationForAttributeName + "," + ValidationErrorClassName)]
        public class ValidationClassTagHelper : TagHelper
        {
            private const string ValidationForAttributeName = "pf-validation-for";
            private const string ValidationErrorClassName = "pf-validationerror-class";

            [HtmlAttributeName(ValidationForAttributeName)]
            public ModelExpression For { get; set; }

            [HtmlAttributeName(ValidationErrorClassName)]
            public string ValidationErrorClass { get; set; }

            [HtmlAttributeNotBound]
            [ViewContext]
            public ViewContext ViewContext { get; set; }

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                ViewContext.ViewData.ModelState.TryGetValue(For.Name, out ModelStateEntry entry);

                if (entry == null || !entry.Errors.Any()) return;

                var tagBuilder = new TagBuilder("div");

                tagBuilder.AddCssClass(ValidationErrorClass);
                output.MergeAttributes(tagBuilder);
            }
        }
    }
}
