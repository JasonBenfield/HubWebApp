import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { GridListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";

export class InstallConfigurationItem extends GridListGroupItemView {
    static 

    readonly templateNameTextView: BasicTextComponentView;
    readonly machineNameTextView: BasicTextComponentView;
    readonly domainTextView: BasicTextComponentView;
    readonly siteNameTextView: BasicTextComponentView;
    readonly configurationNameTextView: BasicTextComponentView;

    constructor(container: BasicComponentView) {
        super(container);
        const cell1 = this.addCell();
        const cell2 = this.addCell();
        const cell3 = this.addCell();
        const cell4 = this.addCell();
        const cell5 = this.addCell();
    }
}