import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { AppListItemView } from "./AppListItemView";

export class AppListItem {
    constructor(readonly app: IAppModel, appRedirectUrl: (appID: number) => string, view: AppListItemView) {
        new TextBlock(app.AppName, view.appName);
        new TextBlock(app.Title, view.appTitle);
        new TextBlock(app.Type.DisplayText, view.appType);
        view.setHref(appRedirectUrl(app.ID));
    }
}