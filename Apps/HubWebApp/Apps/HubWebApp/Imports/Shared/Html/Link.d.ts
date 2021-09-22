import { HtmlContainerComponent } from "./HtmlContainerComponent";
import { LinkViewModel } from "./LinkViewModel";
export declare class Link extends HtmlContainerComponent {
    constructor(vm?: LinkViewModel);
    private onClick;
    protected readonly vm: LinkViewModel;
    readonly clicked: import("../Events").DefaultEventHandler<any>;
    private href;
    setHref(href: string): void;
    enable(): void;
    disable(): void;
}
