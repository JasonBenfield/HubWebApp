import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { UnorderedList } from "XtiShared/Html/UnorderedList";
import { TextBlock } from "XtiShared/Html/TextBlock";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";

export class ModCategoryComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Modifier Category');
        this.alert = this.addCardAlert().alert;
        this.modCategoryName = this.addCardBody()
            .addContent(new UnorderedList())
            .addItem()
            .addContent(new TextBlock());
    }

    private modCategoryID: number;

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
        this.vm.name('');
    }

    private readonly alert: MessageAlert;
    private readonly modCategoryName: TextBlock;

    async refresh() {
        let modCategory = await this.getModCategory(this.modCategoryID);
        this.modCategoryName.setText(modCategory.Name);
    }

    private async getModCategory(modCategoryID: number) {
        let modCategory: IModifierCategoryModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategory = await this.hubApi.ModCategory.GetModCategory(modCategoryID);
            }
        );
        return modCategory;
    }
}