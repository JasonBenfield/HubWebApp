import { ButtonViewModel } from "../Html/ButtonViewModel";
import { Button } from "../Html/Button";
import { FaIcon } from "../FaIcon";
import { ICommandItem } from "./CommandItem";
export declare class ButtonCommandItem extends Button implements ICommandItem {
    static offscreenSubmit(vm: ButtonViewModel): ButtonCommandItem;
    constructor(vm?: ButtonViewModel);
    readonly executeRequested: import("../Events").DefaultEventHandler<any>;
    readonly icon: FaIcon;
    private readonly textSpan;
    positionIconRight(): void;
    setText(text: string): void;
    private active;
    setActive(): void;
    setInactive(): void;
    private updateActiveCss;
    protected readonly vm: ButtonViewModel;
    changeTypeToSubmit(): void;
    makeOffscreenSubmit(): void;
}
