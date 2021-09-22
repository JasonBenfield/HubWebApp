import { LinkListItemViewModel } from "./LinkListItemViewModel";
import { ListGroupItem } from "./ListGroupItem";
export declare class LinkListGroupItem extends ListGroupItem {
    constructor(vm: LinkListItemViewModel);
    private readonly link;
    setHref(href: string): void;
    enable(): void;
    disable(): void;
}
