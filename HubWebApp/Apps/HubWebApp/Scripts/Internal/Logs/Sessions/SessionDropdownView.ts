import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { DropdownComponentView } from "@jasonbenfield/sharedwebapp/Views/Dropdown";
import { GridCellView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";
import { DropdownMenuView } from "@jasonbenfield/sharedwebapp/Views/Dropdown";

export class SessionDropdownView extends GridCellView {
    readonly requestLink: TextLinkView;

    constructor(container: BasicComponentView) {
        super(container);
        const dropdown = this.addView(DropdownComponentView);
        dropdown.button.addView(TextSpanView).setText('View');
        dropdown.button.useOutlineStyle(ContextualClass.secondary);
        const menu = dropdown.menuContainer.addView(DropdownMenuView);
        const requestDropdownLink = menu.addTextLinkItem();
        requestDropdownLink.link.setText('Requests');
        this.requestLink = requestDropdownLink.link;
    }
}