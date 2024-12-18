import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { App } from "../../Lib/App";
import { AppListItemView } from "./AppListItemView";

export class AppListItem extends LinkComponent {
    constructor(
        readonly app: App,
        appRedirectUrl: string,
        view: AppListItemView
    ) {
        super(view);
        const appNameTextComponent = this.addComponent(new TextComponent(view.appName))
        const appKeyTextCompnent = this.addComponent(new TextComponent(view.appTitle))
        const appTypeTextComponent = this.addComponent(new TextComponent(view.appType))
        this.setHref(appRedirectUrl);
        appNameTextComponent.setText(app.appKey.name.displayText);
        appKeyTextCompnent.setText(`${app.appKey.format()}`);
        appTypeTextComponent.setText(app.appKey.type.DisplayText);
    }
}