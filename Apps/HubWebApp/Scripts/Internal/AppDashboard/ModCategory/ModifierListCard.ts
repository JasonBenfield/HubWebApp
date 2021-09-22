import { Card } from "XtiShared/Card/Card";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModifierListItem } from "./ModifierListItem";

export class ModifierListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        vm.title('Modifiers');
    }

    private modCategoryID: number;

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    private readonly alert: MessageAlert;
    private readonly modifiers: CardButtonListGroup;

    async refresh() {
        let modifiers = await this.getModifiers();
        this.modifiers.setItems(
            modifiers,
            (sourceItem, listItem) => {
                listItem.setData(sourceItem);
                listItem.addContent(new ModifierListItem(sourceItem));
            }
        );
        if (modifiers.length === 0) {
            this.alert.danger('No Modifiers were Found');
        }
    }

    private async getModifiers() {
        let modifiers: IModifierModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modifiers = await this.hubApi.ModCategory.GetModifiers(this.modCategoryID);
            }
        );
        return modifiers;
    }
}