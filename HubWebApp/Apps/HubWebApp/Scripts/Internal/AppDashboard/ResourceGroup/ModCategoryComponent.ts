import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ModCategoryComponentView } from "./ModCategoryComponentView";

export class ModCategoryComponent {
    private groupID: number;

    private readonly alert: MessageAlert;
    private readonly modCategoryName: TextComponent;

    private modCategory: IModifierCategoryModel;

    private readonly _clicked = new DefaultEvent<IModifierCategoryModel>(this);
    readonly clicked = this._clicked.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ModCategoryComponentView
    ) {
        new TextComponent(view.titleHeader).setText('Modifier Category');
        this.alert = new CardAlert(view.alert).alert;
        this.modCategoryName = new TextComponent(view.modCategoryName);
        new ListGroup(view.listGroup).registerItemClicked(this.onClicked.bind(this));
    }

    private onClicked() {
        this._clicked.invoke(this.modCategory);
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

    private async getModCategory(groupID: number) {
        let modCategory: IModifierCategoryModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategory = await this.hubApi.ResourceGroup.GetModCategory({
                    VersionKey: 'Current',
                    GroupID: groupID
                });
            }
        );
        return modCategory;
    }
}