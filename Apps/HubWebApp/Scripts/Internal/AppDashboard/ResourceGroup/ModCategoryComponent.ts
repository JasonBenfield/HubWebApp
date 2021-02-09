import { DefaultEvent } from "XtiShared/Events";
import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { ButtonListGroup } from "XtiShared/ListGroup/ButtonListGroup";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";

export class ModCategoryComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Modifier Category');
        this.alert = this.addCardAlert().alert;
        this.listGroup = this.addButtonListGroup();
        this.modCategoryName = this.listGroup
            .addItem()
            .addContent(new TextSpan());
        this.listGroup.itemClicked.register(this.onClicked.bind(this));
    }

    private readonly listGroup: ButtonListGroup;
    private readonly modCategoryName: TextSpan;

    private readonly _clicked = new DefaultEvent<IModifierCategoryModel>(this);
    readonly clicked = this._clicked.handler();

    private onClicked() {
        this._clicked.invoke(this.modCategory);
    }

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
        this.listGroup.hide();
    }

    private readonly alert: MessageAlert;

    private modCategory: IModifierCategoryModel;

    async refresh() {
        this.modCategory = await this.getModCategory(this.groupID);
        this.modCategoryName.setText(this.modCategory.Name);
        this.listGroup.show();
    }

    private async getModCategory(groupID: number) {
        let modCategory: IModifierCategoryModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategory = await this.hubApi.ResourceGroup.GetModCategory(groupID);
            }
        );
        return modCategory;
    }
}