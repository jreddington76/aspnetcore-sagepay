using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace aspnetcore_sagepay.TagHelpers
{
    [HtmlTargetElement("span", Attributes = DescriptionForAttributeName)]
    public class SpanDescriptionTagHelper : TagHelper
    {
        private const string DescriptionForAttributeName = "asp-description-for";

        public SpanDescriptionTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override int Order
        {
            get
            {
                return -1000;
            }
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(DescriptionForAttributeName)]
        public ModelExpression DescriptionFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var metadata = DescriptionFor.Metadata ?? throw new InvalidOperationException($"No provided metadata ({DescriptionForAttributeName})");

            output.Attributes.SetAttribute("id", metadata.PropertyName + "-description");

            if (!string.IsNullOrWhiteSpace(metadata.Description))
            {
                output.Content.SetContent(metadata.Description);
                output.TagMode = TagMode.StartTagAndEndTag;
            }
        }
    }
}