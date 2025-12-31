namespace StoreApp.web.TagHelpers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.web.Models;


[HtmlTargetElement("div",Attributes ="page-model")]

public class PageLinkTagHelper:TagHelper
{
    public readonly IUrlHelperFactory _urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    public PageInfo? PageModel { get; set; }

    public string? PageAction { get; set; }

    public string PageClass { get; set; }=string.Empty;
    public string PageClassLink { get; set; }=string.Empty;

    public string PageClassActive { get; set; }=string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if(ViewContext!=null && PageModel!=null)
      {
        IUrlHelper urlHelper=_urlHelperFactory.GetUrlHelper(ViewContext);
        TagBuilder result=new TagBuilder("div");

        for(int i=1;i<=PageModel.TotalPages;i++)
        {
            TagBuilder link=new TagBuilder("a");
           link.Attributes["href"]=urlHelper.Action(PageAction,new {page=i});
           link.AddCssClass(PageClass);
           link.AddCssClass(i==PageModel.CurrentPage ? PageClassActive : PageClassLink);
           link.InnerHtml.Append(i.ToString());
            result.InnerHtml.AppendHtml(link);
        }

        output.Content.AppendHtml(result.InnerHtml);
      }

       
    }

   

    
}   
