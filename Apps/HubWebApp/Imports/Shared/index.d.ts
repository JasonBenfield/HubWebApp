
declare module '*.scss' {
    const content: { [className: string]: string };
    export default content;
}

declare module '*.html' {
    const content: string;
    export default content;
}

declare type LayoutBreakpoint = 'xs' | 'sm' | 'md' | 'lg' | 'xl' | 'xxl';

interface IEmptyRequest {
}

interface PageContext {
    BaseUrl: string;
    CacheBust: string;
    EnvironmentName: string;
    AppTitle: string;
    PageTitle: string;
    IsAuthenticated: boolean;
    UserName: string;
}

declare let pageContext: PageContext;

interface IPageViewModel {
}

interface IPageFrame extends IAggregateComponent {
    load();
}

declare module 'xtistart' {
    export class Startup {
        build();
    }
}

interface EventCallback<TArgs> {
    (args: TArgs, source?: any): void;
}

interface IArrayItemEvent<TArgs> {
    handlerAccessor: (item: any) => IEventHandler<TArgs>;
    callback: EventCallback<TArgs>;
    isEnabled?: () => boolean;
}

interface IEventHandler<TArgs> {
    register(callback: EventCallback<TArgs>, identifier?: any, isEnabled?: () => boolean): IEventHandler<TArgs>;
    unregister(identifier: any);
}

interface IEvent<TArgs> extends IEventHandler<TArgs> {
    invoke(args?: TArgs);
    dispose();
}

interface RegisteredEventCallback {
    callback: EventCallback<any>;
    identifier: string;
    isEnabled: () => boolean;
}

interface DelegatedEvent {
    event: string;
    selector: string;
    callback: (any, string) => boolean;
}

interface IActionErrorOptions {
    caption?: string;
    preventDefault?: boolean;
}

interface IErrorModel {
    Message: string;
    Caption: string;
    Source: string;
}

interface IUserStartRequest {
    ReturnUrl: string;
}

interface IAppApiView<TArgs> {
    getUrl(data: TArgs);
    open(data: TArgs);
    openWindow(data: TArgs);
}

interface IUserGroup {
    readonly Index: IAppApiView<IUserStartRequest>;
}

interface IErrorList {
    add(error: IErrorModel);
    merge(errors: IErrorList);
    hasErrors(): boolean;
    values(): IErrorModel[];
}

interface IField {
    getName(): string;
    getCaption(): string;
    getValue(): any;
    getField(name: string): IField;
    clearErrors();
    validate(errors: IErrorList);
    import(values: Record<string, any>);
    export(values: Record<string, any>);
}

interface IConstraintResult {
    readonly isValid: boolean;
    readonly errorMessage: string;
}

interface IConstraint {
    test(value: any): IConstraintResult;
}

interface IComponentViewModel {
    readonly componentName: ko.Observable<string>;
}

interface IComponentTemplate {
    readonly name: string;
    register();
}

interface IHtmlComponentViewModel extends IComponentViewModel {
    readonly id: ko.Observable<string>;
    readonly name: ko.Observable<string>;
    readonly css: ko.Observable<string>;
    readonly isVisible: ko.Observable<boolean>;
    readonly title: ko.Observable<string>;
}

interface IHtmlContainerComponentViewModel extends IHtmlComponentViewModel {
    readonly content: IAggregateComponentViewModel;
}

interface IAggregateComponent {
    setName(name: string);
    addContent<TItem extends IComponent>(item: TItem): TItem;
    insertContent<TItem extends IComponent>(index: number, item: TItem): TItem;
    addItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(
        itemVM: TItemVM,
        item: TItem
    ): TItem;
    insertItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(
        index: number,
        itemVM: TItemVM,
        item: TItem
    ): TItem;
    removeItem<TItem extends IComponent>(item: TItem);
    show();
    hide();
}

interface IAggregateComponentViewModel extends IComponentViewModel {
    readonly name: ko.Observable<string>;
    readonly items: ko.ObservableArray<IComponentViewModel>;
    readonly isVisible: ko.Observable<boolean>;
}

interface IList {
    readonly itemClicked: IEventHandler<IListItem>;
    addListItem<TListItem extends IListItem>(itemVM: IListItemViewModel, item: TListItem): TListItem;
}

interface IListViewModel extends IHtmlComponentViewModel {
    readonly itemClicked: IEventHandler<IListItemViewModel>;
    readonly items: ko.ObservableArray<IListItemViewModel>;
    readonly hasItems: ko.Observable<boolean>;
}

interface IListItemViewModel extends IHtmlComponentViewModel {
    readonly content: IAggregateComponentViewModel;
    readonly isClickable: boolean;
}

interface IListItem {
    readonly content: IAggregateComponent;

    getData<T>(): T;

    setData(data: any);

    addCssName(name: string);

    addContent<TItem extends IComponent>(item: TItem): TItem;

    addToList(list: IList): this;

    show();

    hide();
}

type ColumnCssSize = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 'auto' | 'fill';

interface IColumnCss {
    xs(columnSize: ColumnCssSize);
    sm(columnSize: ColumnCssSize);
    md(columnSize: ColumnCssSize);
    lg(columnSize: ColumnCssSize);
    xl(columnSize: ColumnCssSize);
    xxl(columnSize: ColumnCssSize);
}

interface ITextCss {
}

interface IColumn {
    setColumnCss(columnCss: IColumnCss);
    setTextCss(textCss: ITextCss);
}

interface IFormGroup extends IComponent {
    readonly captionColumn: IColumn;
    readonly valueColumn: IColumn;
}

interface IFormGroupField extends IFormGroup, IField {
}

interface IComponent {
    addToContainer(container: IAggregateComponent): this;
    insertIntoContainer(container: IAggregateComponent, index: number): this;
    removeFromContainer(container: IAggregateComponent): this;
}

declare type constructor<T> = {
    new(...args: any[]): T;
};