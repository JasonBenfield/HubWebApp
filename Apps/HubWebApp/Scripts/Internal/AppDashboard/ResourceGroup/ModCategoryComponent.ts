import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponentView } from "./ModCategoryComponentView";

export class ModCategoryComponent {
    private groupID: number;

    private readonly alert: MessageAlert;
    private readonly modCategoryName: TextBlock;

    private modCategory: IModifierCategoryModel;

    private readonly _clicked = new DefaultEvent<IModifierCategoryModel>(this);
    readonly clicked = this._clicked.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ModCategoryComponentView
    ) {
        new TextBlock('Modifier Category', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.modCategoryName = new TextBlock('', this.view.modCategoryName);
        this.view.clicked.register(this.onClicked.bind(this));
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
        this.modCategoryName.setText(this.modCategory.Name);
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