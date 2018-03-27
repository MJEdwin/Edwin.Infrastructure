using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BingoCore.Infrastructure.TagHelpers.Bootstrap
{
    [HtmlTargetElement("bg:modal", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ModalTagHelper : TagHelper
    {
        public string Width { get; set; } = null;

        public bool Fade { get; set; } = true;

        public string Description { get; set; } = null;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"modal {(Fade?"fade":"")}");
            //output.Attributes.SetAttribute("tabindex", "-1");
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("aria-hidden", true);
            if (!string.IsNullOrEmpty(Description))
                output.Attributes.SetAttribute("aria-describedby", Description);
            var content = await output.GetChildContentAsync();

            var modalDialog = new TagBuilder("div");
            modalDialog.AddCssClass("modal-dialog");
            modalDialog.MergeAttribute("role", "document");
            if (!string.IsNullOrEmpty(Width))
            {
                modalDialog.MergeAttribute("style", $"width:{Width}");
            }

            var contentDialog = new TagBuilder("div");
            contentDialog.AddCssClass("modal-content");

            contentDialog.InnerHtml.AppendHtml(content);
            modalDialog.InnerHtml.AppendHtml(contentDialog);

            output.Content.SetHtmlContent(modalDialog);
        }
    }

    [HtmlTargetElement("bg:modal-header", ParentTag = "bg:modal",TagStructure =TagStructure.WithoutEndTag)]
    public class ModalHeaderTagHelper : TagHelper
    {
        public string Title { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", "modal-header");

            var button = new TagBuilder("button");
            button.AddCssClass("close");
            button.MergeAttribute("data-dismiss", "modal");
            button.MergeAttribute("aria-hidden", "true");

            var title = new TagBuilder("h4");
            title.AddCssClass("modal-title");
            title.InnerHtml.AppendHtml(Title);

            output.Content.SetHtmlContent(button);
            output.Content.AppendHtml(title);
        }
    }

    [HtmlTargetElement("bg:modal-body", ParentTag = "bg:modal", TagStructure =TagStructure.NormalOrSelfClosing)]
    public class ModalBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", "modal-body");
            var body = await output.GetChildContentAsync();

            output.Content.SetHtmlContent(body);
        }
    }

    [HtmlTargetElement("bg:modal-footer", ParentTag = "bg:modal", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ModalFooterTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", "modal-footer");
            var footer = await output.GetChildContentAsync();

            output.Content.SetHtmlContent(footer);
        }
    }
}
