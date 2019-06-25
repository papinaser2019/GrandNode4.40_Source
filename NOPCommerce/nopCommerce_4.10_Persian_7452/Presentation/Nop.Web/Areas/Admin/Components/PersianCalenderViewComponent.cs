using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace Nop.Web.Areas.Admin.Components
{
    /// <summary>
    /// Represents a view component that displays an admin widgets
    /// </summary>
    public class PersianCalenderViewComponent : NopViewComponent
    {
        private readonly ICategoryService icategoryService_0;
        private readonly IStoreContext _istoreContext;
        private readonly IWorkContext _iworkContext;
        private readonly IWebHelper _webHelper;
        private readonly IPictureService ipictureService_0;
        private readonly ILocalizationService ilocalizationService_0;
        private readonly MediaSettings mediaSettings_0;
        private readonly ICacheManager icacheManager_0;
        public ShoppingCartSettings _shoppingCartSettings { get; set; }

        public IProductService _productService { get; set; }

        public IProductModelFactory _productModelFactory { get; set; }

        public PersianCalenderViewComponent(ICategoryService iCategoryService,
            IStoreContext storeContext, IWorkContext iworkContext,
            IPictureService ipictureService,
            ILocalizationService ilocalizationService,
            MediaSettings mediaSettings,
            ICacheManager icacheManager,
            ShoppingCartSettings shoppingCartSettings,
            IProductService productService,
            IProductModelFactory productModelFactory,
            IWebHelper webHelper)
        {
            this.icategoryService_0 = iCategoryService;
            this._istoreContext = storeContext;
            this._iworkContext = iworkContext;
            this.ipictureService_0 = ipictureService;
            this.ilocalizationService_0 = ilocalizationService;
            this.mediaSettings_0 = mediaSettings;
            this.icacheManager_0 = icacheManager;
            this._webHelper = webHelper;
            this._shoppingCartSettings = shoppingCartSettings;
            this._productService = productService;
            this._productModelFactory = productModelFactory;
        }
        #region Methods

        /// <summary>
        /// Invoke view component
        /// </summary>
        /// <param name="widgetZone">Widget zone name</param>
        /// <param name="additionalData">Additional data</param>
        /// <returns>View component result</returns>
        [Produces("text/html")]
        public IViewComponentResult Invoke(string widgetZone, object additionalData = null)
        {
            //prepare model
            if (this._iworkContext.WorkingLanguage.LanguageCulture != "fa-IR")
            {
                return Content("");
            }
            //if (NFMessageTokenProvider.RoutePersianCalneder(Request))
            {
                if (widgetZone == "productdetails_overview_top" && additionalData != null)
                {
                    int updatecartitemid = 0;
                    StringValues stringValues;
                    if (base.HttpContext.Request.Query.TryGetValue("updatecartitemid", out stringValues))
                    {
                        updatecartitemid = int.Parse(stringValues);
                    }
                    ShoppingCartItem shoppingCartItem = null;
                    if (this._shoppingCartSettings.AllowCartItemEditing && updatecartitemid > 0)
                    {
                        List<ShoppingCartItem> source = ShoppingCartExtensions.LimitPerStore(this._iworkContext.CurrentCustomer.ShoppingCartItems, this._istoreContext.CurrentStore.Id).ToList<ShoppingCartItem>();
                        shoppingCartItem = source.FirstOrDefault((ShoppingCartItem x) => x.Id == updatecartitemid);
                    }
                    int num = (int)additionalData;
                    Product productById = this._productService.GetProductById(num);
                    ProductDetailsModel productDetailsModel = this._productModelFactory.PrepareProductDetailsModel(productById, shoppingCartItem, false);
                    if (PersianCalenderViewComponent.co__6.cp__0 == null)
                    {
                        PersianCalenderViewComponent.co__6.cp__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Status", typeof(PersianCalenderViewComponent), new CSharpArgumentInfo[]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
                        }));
                    }
                    PersianCalenderViewComponent.co__6.cp__0.Target(PersianCalenderViewComponent.co__6.cp__0, base.ViewBag, productDetailsModel.IsRental ? "rental" : "");
                    return base.View<ProductDetailsModel>("~/Plugins/NopFarsi.Plugin.PersianCalender/Views/ProductAttributesAndRentalInfo.cshtml", productDetailsModel);
                }
                else
                if (widgetZone == "admin_header_before")
                    ViewBag.result = ("<span  class='L3' nop='4n21' </span>");
                else if (widgetZone == "admin_header_navbar_before")
                    ViewBag.result = ("<input id='nf' type='hidden' value='" + (DateTime.Now.Second * 7) + "' />");
                else if (widgetZone == "admin_header_toggle_after")

                    ViewBag.result = ("<script id='d11' src='/Plugins/NopFarsi.Plugin.PersianCalender/Scripts/kendo.web.min.js'></script>");


                else
                    ViewBag.result = ("<!--generate nopfarsi.ir -->");
                return View("~/Plugins/NopFarsi.Plugin.PersianCalender/Views/PublicInfo.cshtml");
            }
            return Content("<!--nopFarsi.ir Persian Calender-->");
        }

        private class co__6
        {
            public static CallSite<Func<CallSite, object, string, object>> cp__0 { get; set; }
        }
        #endregion
    }
}