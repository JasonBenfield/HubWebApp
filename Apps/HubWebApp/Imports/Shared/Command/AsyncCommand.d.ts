import { FaIconAnimation } from "../FaIcon";
import { ButtonViewModel } from "../Html/ButtonViewModel";
import { ButtonCommandItem } from "./ButtonCommandItem";
import { ICommandItem } from "./CommandItem";
export declare type CommandAction = (context?: any) => any;
export declare class AsyncCommand {
    private readonly action;
    constructor(action: CommandAction);
    configure(action: (c: AsyncCommand) => void): this;
    private readonly items;
    addButton(vm: ButtonViewModel): ButtonCommandItem;
    add<T extends ICommandItem>(item: T): T;
    private isMultiExecutionAllowed;
    private isEnabled;
    private executionCount;
    private inProgressAnimation;
    animateIconWhenInProgress(inProgressAnimation: FaIconAnimation): void;
    private icons;
    private forEachItem;
    activate(): void;
    deactivate(): void;
    allowMultiExecution(): void;
    show(): void;
    hide(): void;
    enable(): void;
    disable(): void;
    private updateIsEnabled;
    execute(context?: any): Promise<void>;
    private canExecute;
}
