import { TextSmall } from "./Html/TextSmall";
import { TextSpan } from "./Html/TextSpan";
import { Toolbar } from "./Html/Toolbar";
import { PageViewModel } from "./PageViewModel";
import { ModalErrorComponent } from "./Error/ModalErrorComponent";
import { Block } from "./Html/Block";
import { apiConstructor, AppApi } from "./AppApi";
export declare type pageConstructor<TPage extends PageFrame> = {
    new (vm: PageViewModel): TPage;
};
export declare class PageFrame implements IPageFrame {
    private readonly vm;
    readonly toolbar: Toolbar;
    readonly appTitle: TextSpan;
    readonly pageTitle: TextSmall;
    constructor(vm?: PageViewModel);
    private readonly logoutMenuItem;
    setName(name: string): void;
    addItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(itemVM: TItemVM, item: TItem): TItem;
    insertItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(index: number, itemVM: TItemVM, item: TItem): TItem;
    removeItem<TItem extends IComponent>(item: TItem): any;
    show(): void;
    hide(): void;
    private readonly outerContent;
    readonly content: Block;
    readonly modalError: ModalErrorComponent;
    insertContent<TItem extends IComponent>(index: number, item: TItem): TItem;
    addContent<TItem extends IComponent>(item: TItem): TItem;
    load(): void;
    private apiFactory;
    defaultApi(): AppApi;
    setDefaultApiType(defaultApi: apiConstructor<AppApi>): void;
    api<TApi extends AppApi>(apiCtor: apiConstructor<TApi>): TApi;
}
