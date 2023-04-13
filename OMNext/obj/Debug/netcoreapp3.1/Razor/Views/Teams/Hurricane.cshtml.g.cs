#pragma checksum "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cd45cbadf5c2ee466fc963d6886afe5d8cf26d36"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Teams_Hurricane), @"mvc.1.0.view", @"/Views/Teams/Hurricane.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\_ViewImports.cshtml"
using OMNext;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\_ViewImports.cshtml"
using OMNext.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd45cbadf5c2ee466fc963d6886afe5d8cf26d36", @"/Views/Teams/Hurricane.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56742bbda1a0b1698dde0f28222d931a9d9b55a2", @"/Views/_ViewImports.cshtml")]
    public class Views_Teams_Hurricane : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OMNext.Models.DataDrive>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/videos/lavaflow.mp4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("video/mp4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_MedCommBulletin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
  
    ViewData["Title"] = "Hurricane";
    ViewBag.Password = ViewData["Password"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <h2 class=\"light-text\">");
#nullable restore
#line 8 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                      Write(ViewData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n");
            WriteLiteral(@"    <div id=""standingBy"" class=""bg-danger form-control"">
        <marquee><p style=""font-size: 14pt;"">Error retrieving satellite data.  Please stand by for instruction from Mission Control.</p></marquee>
    </div>

    <div id=""mainDiv"" class=""col-md-12 bg-transparent opac-85 py-4 d-none"">
");
            WriteLiteral(@"        <ul class=""nav nav-tabs"">
            <li id=""li_archive"" class=""nav-item bg-light d-none""><a data-toggle=""tab"" role=""tab"" href=""#archive"" class=""nav-link"">Archived Data</a></li>
            <li id=""li_realtime"" class=""nav-item bg-light""><a data-toggle=""tab"" role=""tab"" href=""#realtime"" class=""nav-link"">Real-Time Data</a></li>
            <li id=""li_video"" class=""nav-item bg-light d-none""><a data-toggle=""tab"" role=""tab"" href=""#video"" class=""nav-link"">Volcano Video</a></li>
            <li id=""li_questions"" class=""nav-item bg-light d-none""><a data-toggle=""tab"" role=""tab"" href=""#questions"" class=""nav-link"">Post-Brief Questions</a></li>
        </ul>

        <div class=""tab-content border"">
            <div id=""archive"" role=""tabpanel"" class=""tab-pane bg-white opac-85 p-2"" style=""height: 740px"">
");
            WriteLiteral("                ");
#nullable restore
#line 27 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
           Write(Html.Raw(ViewBag.ArchivedHurData));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div id=\"realtime\" role=\"tabpanel\" class=\"tab-pane container\">\r\n                <div style=\"height: 740px\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col col-7 opac-85 h-100 w-100\">\r\n");
            WriteLiteral("                            ");
#nullable restore
#line 34 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                       Write(await Component.InvokeAsync("FDDataPush", new { MissionID = ViewData["MissionID"], teamid = 1 }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                        <div class=\"h-100 w-100 col col-5 p-0 m-0\">\r\n                            <div id=\"EvacDataDriveData\" class=\"bg-white opac-85\">\r\n");
            WriteLiteral("                                ");
#nullable restore
#line 39 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                           Write(await Component.InvokeAsync("DataDriveData", new { MissionID = ViewData["MissionID"], team = "Evacuation" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n");
#nullable restore
#line 41 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                               if ((bool)TempData.Peek("HasMedComm"))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div id=\"MedCommDataDriveData\" class=\"bg-white opac-85 mt-4\" style=\"overflow-y: auto\">\r\n                                        ");
#nullable restore
#line 44 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                                   Write(await Component.InvokeAsync("DataDriveData", new { MissionID = ViewData["MissionID"], team = "MedComm" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </div>\r\n");
#nullable restore
#line 46 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                                }
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div id=\"HurcChat\" class=\"h-100 w-100 border bg-white opac-85 p-2 mt-4\" style=\"overflow: hidden;\">\r\n");
            WriteLiteral("                                ");
#nullable restore
#line 50 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                           Write(await Component.InvokeAsync("TeamChat", new { MissionID = ViewData["MissionID"] }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id=""video"" role=""tabpanel"" class=""tab-pane"" style=""height: 740px"">
                <video width=""1080"" height=""740"" controls>
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("source", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cd45cbadf5c2ee466fc963d6886afe5d8cf26d369465", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    Your browser does not support the video tag.\r\n                </video>\r\n            </div>\r\n            <div id=\"questions\" role=\"tabpanel\" class=\"tab-pane bg-white opac-85 p-2\" style=\"height: 740px\">\r\n                ");
#nullable restore
#line 63 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
           Write(Html.Raw(ViewBag.PostBrief));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cd45cbadf5c2ee466fc963d6886afe5d8cf26d3611131", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<script type=\"text/javascript\">\r\n    $(document).ready(function () {\r\n        InitInterface(\'");
#nullable restore
#line 72 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Hurricane.cshtml"
                  Write(ViewBag.Password);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n    });\r\n</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OMNext.Models.DataDrive> Html { get; private set; }
    }
}
#pragma warning restore 1591