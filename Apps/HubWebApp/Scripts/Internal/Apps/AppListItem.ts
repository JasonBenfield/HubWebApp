﻿import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { AppListItemView } from "./AppListItemView";

export class AppListItem {
    constructor(readonly appWithModKey: IAppWithModKeyModel, appRedirectUrl: (modKey: string) => string, view: AppListItemView) {
        new TextBlock(appWithModKey.App.AppName, view.appName);
        new TextBlock(appWithModKey.App.Title, view.appTitle);
        new TextBlock(appWithModKey.App.Type.DisplayText, view.appType);
        view.setHref(appRedirectUrl(appWithModKey.ModKey));
    }
}