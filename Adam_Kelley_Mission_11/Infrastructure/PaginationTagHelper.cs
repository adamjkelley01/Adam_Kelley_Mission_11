using Adam_Kelley_Mission_11.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Adam_Kelley_Mission_11.Infrastructure
{
    // This TagHelper targets <div> elements with the "page-model" attribute
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        // Constructor for PaginationTagHelper
        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            urlHelperFactory = temp;
        }

        // Properties
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public string? PageAction { get; set; }

        public PaginationInfo PageModel { get; set; }

        public bool PageClassesEnabled { get; set; } = false;

        public string PageClass { get; set; } = String.Empty;

        public string PageClassNormal { get; set; } = String.Empty;

        public string PageClassSelected { get; set; } = String.Empty;

        // Process method for generating pagination HTML
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                // Get the URL helper
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

                // Create a new <div> tag
                TagBuilder result = new TagBuilder("div");

                // Loop through each page and create an <a> tag for it
                for (int i = 1; i <= PageModel.TotalNumPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");

                    // Set the href attribute of the <a> tag
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });

                    // Add CSS classes based on PageClassesEnabled and the current page
                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    // Set the inner HTML of the <a> tag to the page number
                    tag.InnerHtml.Append(i.ToString());

                    // Append the <a> tag to the result <div>
                    result.InnerHtml.AppendHtml(tag);
                }

                // Append the content of the result <div> to the output
                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
