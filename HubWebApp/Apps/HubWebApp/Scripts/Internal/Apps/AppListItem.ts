import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { App } from "../../Lib/App";
import { AppListItemView } from "./AppListItemView";

export class AppListItem extends BasicComponent {
    constructor(
        readonly app: App,
        appRedirectUrl: string,
        view: AppListItemView
    ) {
        super(view);
        new TextComponent(view.appName).setText(app.appKey.name.displayText);
        new TextComponent(view.appTitle).setText(`${app.appKey.format()}`);
        new TextComponent(view.appType).setText(app.appKey.type.DisplayText);
        new LinkComponent(view).setHref(appRedirectUrl);
    }
}