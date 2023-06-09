#pragma checksum "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "283fc9e1fb3e161e0243485c7fc4baecc61dce70"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Administrators_DisplayCLCUsage), @"mvc.1.0.view", @"/Views/Administrators/DisplayCLCUsage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"283fc9e1fb3e161e0243485c7fc4baecc61dce70", @"/Views/Administrators/DisplayCLCUsage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56742bbda1a0b1698dde0f28222d931a9d9b55a2", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Administrators_DisplayCLCUsage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OMNext.Models.RunningMission>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"container\" style=\"height: 740px; overflow-y: auto;\">\r\n    <h2 class=\"light-text\">Usage By CLC Center Between ");
#nullable restore
#line 7 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                                                  Write(ViewBag.StartDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" and ");
#nullable restore
#line 7 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                                                                         Write(ViewBag.EndDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
    <div class=""row"">
        <div class=""col-md-5 pt-2 bg-light opac-85"">
            <h4>Usage by Date</h4>
            <table>
                <thead>
                    <tr>
                        <th class=""pl-2 pr-2"">Abbreviation</th>
                        <th class=""pl-2 pr-2"">Flight Director</th>
                        <th class=""pl-2 pr-2"">Mission Date</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 20 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td class=\"pl-2 pr-2\">");
#nullable restore
#line 23 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                                             Write(Html.DisplayFor(modelItem => item.CLCCenter.Abbreviation));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"pl-2 pr-2\">");
#nullable restore
#line 24 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                                             Write(Html.DisplayFor(modelItem => item.FlightDirector));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"pl-2 pr-2\">");
#nullable restore
#line 25 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                                             Write(Html.DisplayFor(modelItem => item.MissionDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 27 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n        <div class=\"col-md-7 bg-light opac-85\">\r\n            ");
#nullable restore
#line 32 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
       Write(await Component.InvokeAsync("DisplayCLCCentersList"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </div>
    </div>
</div>
<div class=""row"" style=""height: 300px; overflow-y: auto;"">
    <div class=""col pt-2 bg-light opac-85"">
        <h4>Mission Count per CLC Center</h4>
        <table>
            <thead>
                <tr>
                    <th class=""pl-2 pr-2"">CLC Center</th>
                    <th class=""pl-2 pr-2"">Mission Count</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 47 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                  
                    Dictionary<string, int> MissionCount = new Dictionary<string, int>();
                    foreach (var item in Model)
                    {
                        if (!MissionCount.ContainsKey(item.CLCCenter.CenterName))
                        {
                            MissionCount.Add(item.CLCCenter.CenterName, Model.Where(x => x.CLCCenterID == item.CLCCenterID).Count());
                        }
                    }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                 foreach (var item in MissionCount)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td class=\"pl-2 pr-2\">");
#nullable restore
#line 60 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                                         Write(item.Key);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td class=\"pl-2 pr-2\">");
#nullable restore
#line 61 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                                         Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 63 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n<div class=\"row form-group bg-light opac-85\">\r\n    <input type=\"button\" class=\"btn btn-default\" value=\"Go Back\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2838, "\"", 2902, 3);
            WriteAttributeValue("", 2848, "location.href=\'", 2848, 15, true);
#nullable restore
#line 69 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\Administrators\DisplayCLCUsage.cshtml"
WriteAttributeValue("", 2863, Url.Action("Index", "Administrators"), 2863, 38, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2901, "\'", 2901, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OMNext.Models.RunningMission>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
