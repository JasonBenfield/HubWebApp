import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EventListItem } from "../EventListItem";
import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";

export class MostRecentErrorEventListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Most Recent Errors');
        this.alert = this.addCardAlert().alert;
        this.errorEvents = this.addButtonListGroup();
    }

    private readonly alert: MessageAlert;
    private readonly errorEvents: CardButtonListGroup;

    private resourceID: number;

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
    }

    async refresh() {
        let errorEvents = await this.getErrorEvents();
        this.errorEvents.setItems(
            errorEvents,
            (sourceItem, listItem) => {
                listItem.addContent(new EventListItem(sourceItem));
            }
        );
        if (errorEvents.length === 0) {
            this.alert.danger('No Errors were Found');
        }
    }

    private async getErrorEvents() {
        let errorEvents: IAppEventModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                errorEvents = await this.hubApi.Resource.GetMostRecentErrorEvents(
                    { ResourceID: this.resourceID, HowMany: 10 }
                );
            }
        );
        return errorEvents;
    }
}