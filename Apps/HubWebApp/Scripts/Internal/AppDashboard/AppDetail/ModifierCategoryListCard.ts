import { DefaultEvent } from "XtiShared/Events";
import { Card } from "XtiShared/Card/Card";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";
import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";

export class ModifierCategoryListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Modifier Categories');
        this.alert = this.addCardAlert().alert;
        this.modCategories = this.addButtonListGroup();
        this.modCategories.itemClicked.register(this.onItemSelected.bind(this));
    }

    private readonly alert: MessageAlert;
    private readonly modCategories: CardButtonListGroup;

    private readonly _modCategorySelected = new DefaultEvent<IModifierCategoryModel>(this);
    readonly modCategorySelected = this._modCategorySelected.handler();

    private onItemSelected(item: IListItem) {
        this._modCategorySelected.invoke(item.getData<IModifierCategoryModel>());
    }

    async refresh() {
        let modCategories = await this.getModCategories();
        this.modCategories.setItems(
            modCategories,
            (sourceItem, listItem) => {
                listItem.setData(sourceItem);
                listItem.addContent(new Row())
                    .addColumn()
                    .addContent(new TextSpan(sourceItem.Name));
            }
        );
        if (modCategories.length === 0) {
            this.alert.danger('No Modifier Categories were Found');
        }
    }

    private async getModCategories() {
        let modCategories: IModifierCategoryModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategories = await this.hubApi.App.GetModifierCategories();
            }
        );
        return modCategories;
    }

}