#pragma checksum "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "823bb1287a2820cceca4423bf9418f2c13c7fdce"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Teams_Evacuation), @"mvc.1.0.view", @"/Views/Teams/Evacuation.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"823bb1287a2820cceca4423bf9418f2c13c7fdce", @"/Views/Teams/Evacuation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56742bbda1a0b1698dde0f28222d931a9d9b55a2", @"/Views/_ViewImports.cshtml")]
    public class Views_Teams_Evacuation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OMNext.Models.DataDrive>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/videos/lavaflow.mp4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("video/mp4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_MedCommBulletin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/evacCanvas.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
  
    ViewData["Title"] = "Evacuation";
    ViewBag.Password = ViewData["Password"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <h2 class=\"light-text\">");
#nullable restore
#line 8 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
                      Write(ViewData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n");
            WriteLiteral("\r\n    <div id=\"mainDiv\" class=\"col-md-12 bg-transparent opac-85 py-4\">\r\n");
            WriteLiteral(@"        <ul class=""nav nav-tabs"">
            <li class=""nav-item bg-light""><a data-toggle=""tab"" role=""tab"" href=""#shelters"" class=""nav-link"">Shelters</a></li>
            <li class=""nav-item bg-light""><a data-toggle=""tab"" role=""tab"" href=""#map"" class=""nav-link"">Evacuation Map</a></li>
            <li id=""li_video"" class=""nav-item bg-light d-none""><a data-toggle=""tab"" role=""tab"" href=""#video"" class=""nav-link"">Volcano Video</a></li>
            <li id=""li_questions"" class=""nav-item bg-light d-none""><a data-toggle=""tab"" role=""tab"" href=""#questions"" class=""nav-link"">Post-Brief Questions</a></li>
        </ul>

        <div class=""tab-content border"">
            <div id=""shelters"" role=""tabpanel"" class=""tab-pane bg-white opac-85 p-2"" style=""height: 740px"">
");
            WriteLiteral("                ");
#nullable restore
#line 27 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
           Write(Html.Raw(ViewBag.EvacShelters));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
            <div id=""map"" role=""tabpanel"" class=""tab-pane container"">
                <div style=""height: 740px"">
                    <div class=""row"">
                        <div class=""col col-7 opac-85 h-100 w-100"">
                            <div class=""btn-group-toggle h-100 align-top"" data-toggle=""buttons"">
                                <label id=""btnTopo"" class=""btn btn-success btn-sm"" onmouseup=""changeMap('btnTopo')"">
                                    <input type=""checkbox"" checked autocomplete=""off"" />Topographic Map
                                </label>
                                <label id=""btnPop"" class=""btn btn-success btn-sm"" onmouseup=""changeMap('btnPop')"">
                                    <input type=""checkbox"" checked autocomplete=""off"" />Population Density
                                </label>
                                <label id=""btnRoad"" class=""btn btn-success btn-sm"" onmouseup=""changeMap('btnRoad')"">
                               ");
            WriteLiteral(@"     <input type=""checkbox"" checked autocomplete=""off"" />Roadways
                                </label>
                            </div>
                            <div class=""img-magnifier-container"">
                                <img id=""theMap""");
            BeginWriteAttribute("src", " src=\"", 2888, "\"", 2894, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""height: 710px; background-color: white;"" />
                                <canvas id=""canvas"" class=""canvas_overlay d-none"">Canvas not supported!</canvas>
                            </div>
                        </div>
                        <div class=""h-100 w-100 col col-5 p-0 m-0"">
                            <div id=""EvacDataDriveData"" class=""bg-white opac-85"" style=""overflow-y: auto"">
");
            WriteLiteral("                                ");
#nullable restore
#line 52 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
                           Write(await Component.InvokeAsync("DataDriveData", new { MissionID = ViewData["MissionID"], team = "Evacuation" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n\r\n");
#nullable restore
#line 55 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
                               if ((bool)TempData.Peek("HasMedComm"))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div id=\"MedCommDataDriveData\" class=\"bg-white opac-85 mt-4\" style=\"overflow-y: auto\">\r\n                                        ");
#nullable restore
#line 58 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
                                   Write(await Component.InvokeAsync("DataDriveData", new { MissionID = ViewData["MissionID"], team = "MedComm" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </div>\r\n");
#nullable restore
#line 60 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
                                }
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div id=\"EvacChat\" class=\"h-100 w-100 border bg-white opac-85 p-2 mt-4\" style=\"overflow: hidden;\">\r\n");
            WriteLiteral("                                ");
#nullable restore
#line 64 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
                           Write(await Component.InvokeAsync("TeamChat", new { MissionID = ViewData["MissionID"] }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
            WriteLiteral("            <div id=\"video\" role=\"tabpanel\" class=\"tab-pane\" style=\"height: 740px\">\r\n                <video width=\"1080\" height=\"740\" controls>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("source", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "823bb1287a2820cceca4423bf9418f2c13c7fdce10691", async() => {
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
            WriteLiteral("\r\n                    Your browser does not support the video tag.\r\n                </video>\r\n            </div>\r\n            <div id=\"questions\" role=\"tabpanel\" class=\"tab-pane bg-white opac-85 p-2\" style=\"height: 740px\">\r\n");
            WriteLiteral("                ");
#nullable restore
#line 79 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
           Write(Html.Raw(ViewBag.PostBrief));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>
<div class=""modal fade"" id=""evacModal"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content bg-light opac-85"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""evacTitle"">Evacuation Plan</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div>
                    <input type=""hidden"" id=""x"" name=""x""");
            BeginWriteAttribute("value", " value=\"", 5869, "\"", 5877, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    <input type=\"hidden\" id=\"y\" name=\"y\"");
            BeginWriteAttribute("value", " value=\"", 5939, "\"", 5947, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    <input type=\"hidden\" id=\"circleSize\" name=\"circleSize\"");
            BeginWriteAttribute("value", " value=\"", 6027, "\"", 6035, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                </div>
                <div class=""row"">
                    <div class=""col"">
                        <label for=""evacFrom"">Location: </label>
                        <input type=""text"" class=""p-2 form-control"" readonly name=""evacFrom"" id=""evacFrom""");
            BeginWriteAttribute("value", " value=\"", 6311, "\"", 6319, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col"">
                        <label for=""evacPop"">Population: </label>
                        <input type=""text"" class=""p-2 form-control"" name=""evacPop"" id=""evacPop""");
            BeginWriteAttribute("value", " value=\"", 6613, "\"", 6621, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col"">
                        <label for=""evacTrans"">Transportation: </label>
                        <input type=""text"" class=""p-2 form-control"" name=""evacTrans"" id=""evacTrans""");
            BeginWriteAttribute("value", " value=\"", 6925, "\"", 6933, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col"">
                        <label for=""evacTo"">Destination: </label>
                        <input type=""text"" class=""p-2 form-control"" name=""evacTo"" id=""evacTo""");
            BeginWriteAttribute("value", " value=\"", 7225, "\"", 7233, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col"">
                        <label for=""evacComplete"">Evacuation Complete?: </label>
                        <input type=""checkbox"" class=""p-2 form-control"" name=""evacComplete"" id=""evacComplete""");
            BeginWriteAttribute("value", " value=\"", 7556, "\"", 7564, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                    </div>
                </div>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Close</button>
                <button id=""btnSendEvacData"" type=""button"" class=""btn btn-primary"" onclick=""updateCircle($('#x').val(), $('#y').val(), $('#circleSize').val(), checkBoxChecked())"">Save</button>
            </div>
            <script>
                function checkBoxChecked() {
                    var x = document.getElementById(""evacComplete"").checked;
                    if (!x) {
                        return ""#ffff00"";
                    } else {
                        return ""#58FA58"";
                    }
                }
            </script>
        </div>
    </div>
</div>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "823bb1287a2820cceca4423bf9418f2c13c7fdce16769", async() => {
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
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "823bb1287a2820cceca4423bf9418f2c13c7fdce17886", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<script type=\"text/javascript\">\r\n    $(document).ready(function () {\r\n        InitInterface(\'");
#nullable restore
#line 152 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Teams\Evacuation.cshtml"
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
