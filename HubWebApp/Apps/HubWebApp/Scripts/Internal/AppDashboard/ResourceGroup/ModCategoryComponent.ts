﻿import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ModCategoryComponentView } from "./ModCategoryComponentView";

type Events = { clicked: IModifierCategoryModel };

export class ModCategoryComponent {
    private readonly alert: MessageAlert;
    private readonly modCategoryName: TextComponent;
    private readonly eventSource = new EventSource<Events>(this, { clicked: null });
    readonly when = this.eventSource.when;

    private groupID: number;
    private modCategory: IModifierCategoryModel;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: ModCategoryComponentView
    ) {
        new TextComponent(view.titleHeader).setText('Modifier Category');
        this.alert = new CardAlert(view.alert).alert;
        this.modCategoryName = new TextComponent(view.modCategoryName);
        new ListGroup(view.listGroup).when.itemClicked.then(this.onClicked.bind(this));
    }

    private onClicked() {
        this.eventSource.events.clicked.invoke(this.modCategory);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
        this.view.hideModCategory();
    }

    async refresh() {
        this.modCategory = await this.getModCategory(this.groupID);
        this.modCategoryName.setText(this.modCategory.Name.DisplayText);
        this.view.showModCategory();
    }

    private getModCategory(groupID: number) {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ResourceGroup.GetModCategory({
                VersionKey: 'Current',
                GroupID: groupID
            })
        );
    }
}