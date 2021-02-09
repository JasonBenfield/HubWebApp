import { Awaitable } from "XtiShared/Awaitable";
import { Result } from "XtiShared/Result";
import { Command } from "XtiShared/Command/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceComponent } from "./ResourceComponent";
import { ResourceAccessCard } from "./ResourceAccessCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";
import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MarginCss } from "XtiShared/MarginCss";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { Toolbar } from "XtiShared/Html/Toolbar";
import { ButtonCommandItem } from "XtiShared/Command/ButtonCommandItem";
import { ContextualClass } from "XtiShared/ContextualClass";
import { PaddingCss } from "XtiShared/PaddingCss";
import { HubTheme } from "../../HubTheme";

export class ResourcePanel extends Block {
    public static readonly ResultKeys = {
        backRequested: 'back-requested'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.resourceComponent = flexFill
            .addContent(new ResourceComponent(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceAccessCard = flexFill
            .addContent(new ResourceAccessCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentRequestListCard = flexFill
            .addContent(new MostRecentRequestListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = flexFill
            .addContent(new MostRecentErrorEventListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        let backButton = this.backCommand.add(
            toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton())
        );
        backButton.setText('Resource Group');
    }

    private readonly resourceComponent: ResourceComponent;
    private readonly resourceAccessCard: ResourceAccessCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;

    setResourceID(resourceID: number) {
        this.resourceComponent.setResourceID(resourceID);
        this.resourceAccessCard.setResourceID(resourceID);
        this.mostRecentRequestListCard.setResourceID(resourceID);
        this.mostRecentErrorEventListCard.setResourceID(resourceID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.resourceComponent.refresh(),
            this.resourceAccessCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        return Promise.all(promises);
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    readonly backCommand = new Command(this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(ResourcePanel.ResultKeys.backRequested));
    }
}