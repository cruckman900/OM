#pragma checksum "C:\Users\cruck\Downloads\OMNext\OMNext\Views\FlightDirectors\Components\FDDataPush\DataPush.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "84e000cde6d5b50ea991c15f3ebd1c9bf206adf8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FlightDirectors_Components_FDDataPush_DataPush), @"mvc.1.0.view", @"/Views/FlightDirectors/Components/FDDataPush/DataPush.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"84e000cde6d5b50ea991c15f3ebd1c9bf206adf8", @"/Views/FlightDirectors/Components/FDDataPush/DataPush.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56742bbda1a0b1698dde0f28222d931a9d9b55a2", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_FlightDirectors_Components_FDDataPush_DataPush : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<style>
    input { width: 125px; }
</style>

<div class=""d-flex flex-row"">
    <div class=""col-sm-4"">
        <input type=""button"" id=""sendArchivedData"" value=""Archived Data"" class=""btn-xs bg-warning"" />
    </div>
    <div class=""col-sm-4"">
        <input type=""button"" id=""sendVolcVideo"" value=""Volcano Video"" class=""btn-xs bg-warning"" />
    </div>
    <div class=""col-sm-4"">
        <input type=""button"" id=""sendPostBrief"" value=""Post Brief"" class=""btn-xs bg-warning"" />
    </div>
</div>
<div class=""d-flex flex-row mt-4"">
    <div class=""col-sm-4"">
        <input type=""number"" id=""volcInterval"" class=""pl-2"" placeholder=""Volc Interval"" step=""1"" min=""3"" max=""10"" />
    </div>
    <div class=""col-sm-4"">
        <input type=""number"" id=""hurcInterval"" class=""pl-2"" placeholder=""Hurc Interval"" step=""1"" min=""3"" max=""10"" />
    </div>
");
#nullable restore
#line 27 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\FlightDirectors\Components\FDDataPush\DataPush.cshtml"
       if ((bool)TempData.Peek("HasMedComm"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-sm-4\">\r\n                <input type=\"number\" id=\"medcommInterval\" class=\"pl-2\" placeholder=\"MedC Interval\" step=\"1\" min=\"3\" max=\"10\" />\r\n            </div>\r\n");
#nullable restore
#line 32 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\FlightDirectors\Components\FDDataPush\DataPush.cshtml"
        }
    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
<div class=""d-flex flex-row"">
    <div class=""col-sm-4"">
        <input type=""button"" id=""sendVolcData"" value=""Send Volc Data"" class=""btn-xs bg-danger"" />
    </div>
    <div class=""col-sm-4"">
        <input type=""button"" id=""sendHurcData"" value=""Send Hurc Data"" class=""btn-xs bg-info"" />
    </div>
");
#nullable restore
#line 42 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\FlightDirectors\Components\FDDataPush\DataPush.cshtml"
       if ((bool)TempData.Peek("HasMedComm"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-sm-4\">\r\n                <input type=\"button\" id=\"sendMCData\" value=\"Send MedC Data\" class=\"btn-xs bg-secondary\" />\r\n            </div>\r\n");
#nullable restore
#line 47 "C:\Users\cruck\Downloads\OMNext\OMNext\Views\FlightDirectors\Components\FDDataPush\DataPush.cshtml"
        }
    

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
