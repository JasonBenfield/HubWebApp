import { AggregateComponent } from "XtiShared/Html/AggregateComponent";
import { AggregateComponentViewModel } from "XtiShared/Html/AggregateComponentViewModel";

export class Panel<TContent extends IComponent> implements IComponent {
    private isActive: boolean = true;
    private readonly container: AggregateComponent;

    constructor(
        readonly content: TContent,
        private readonly vm: AggregateComponentViewModel = new AggregateComponentViewModel()
    ) {
        this.container = new AggregateComponent(vm);
        this.container.addContent(content);
    }

    addToContainer(container: IAggregateComponent) {
        return container.addItem(this.vm, this);
    }

    insertIntoContainer(container: IAggregateComponent, index: number) {
        return container.insertItem(index, this.vm, this);
    }

    removeFromContainer(container: IAggregateComponent) {
        return container.removeItem(this);
    }

    activate() {
        if (!this.isActive) {
            this.isActive = true;
            this.container.show();
        }
    }

    deactivate() {
        if (this.isActive) {
            this.isActive = false;
            this.container.hide();
        }
    }
}