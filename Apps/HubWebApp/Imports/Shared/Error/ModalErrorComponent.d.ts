import { ModalErrorComponentViewModel } from './ModalErrorComponentViewModel';
import { Command } from '../Command/Command';
import { ErrorModel } from '../ErrorModel';
export declare class ModalErrorComponent implements IComponent {
    private readonly vm;
    constructor(vm?: ModalErrorComponentViewModel);
    addToContainer(container: IAggregateComponent): any;
    insertIntoContainer(container: IAggregateComponent, index: number): this;
    removeFromContainer(container: IAggregateComponent): any;
    readonly errorSelected: import("../Events").DefaultEventHandler<ErrorModel>;
    private onClosed;
    show(errors: ErrorModel[], caption?: string): void;
    readonly okCommand: Command;
    private ok;
}
