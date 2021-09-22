export declare class ViewComponent implements IComponent {
    protected readonly view: IComponent;
    constructor(view: IComponent);
    addToContainer(container: IAggregateComponent): this;
    insertIntoContainer(container: IAggregateComponent, index: number): this;
    removeFromContainer(container: IAggregateComponent): this;
}
