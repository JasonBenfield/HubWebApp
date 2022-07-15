﻿import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { BasicComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/BasicComponent";
import { LinkComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/LinkComponent";
import { AppListItemView } from "./AppListItemView";

export class AppListItem extends BasicComponent {
    constructor(
        readonly appWithModKey: IAppWithModKeyModel,
        appRedirectUrl: (modKey: string) => string, view: AppListItemView
    ) {
        super(view);
        new TextComponent(view.appName).setText(appWithModKey.App.AppKey.Name.DisplayText);
        new TextComponent(view.appTitle).setText(appWithModKey.App.Title);
        new TextComponent(view.appType).setText(appWithModKey.App.AppKey.Type.DisplayText);
        new LinkComponent(view).setHref(appRedirectUrl(appWithModKey.ModKey));
    }
}