import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubTheme } from "../HubTheme";

export class AppUserDataPanelView extends GridView {
    readonly alert: MessageAlertView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1));
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
    }
}