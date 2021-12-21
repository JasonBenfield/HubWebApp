import { AppListItemView } from "./AppListItemView";

export class AppListItem {
    constructor(readonly app: IAppModel, appRedirectUrl: (appID: number) => string, view: AppListItemView) {
        view.setAppName(app.AppName);
        view.setAppTitle(app.Title);
        view.setAppType(app.Type.DisplayText);
        view.setHref(appRedirectUrl(app.ID));
    }
}