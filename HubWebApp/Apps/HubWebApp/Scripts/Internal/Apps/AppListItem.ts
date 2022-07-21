import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { AppListItemView } from "./AppListItemView";

export class AppListItem extends BasicComponent {
    constructor(
        readonly app: IAppModel,
        appRedirectUrl: (app: IAppModel) => string, view: AppListItemView
    ) {
        super(view);
        new TextComponent(view.appName).setText(app.AppKey.Name.DisplayText);
        new TextComponent(view.appTitle).setText(app.Title);
        new TextComponent(view.appType).setText(app.AppKey.Type.DisplayText);
        new LinkComponent(view).setHref(appRedirectUrl(app));
    }
}