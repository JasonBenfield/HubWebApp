import { ButtonViewModel } from "./ButtonViewModel";
import { ContextualClass } from "../ContextualClass";
import { HtmlContainerComponent } from "./HtmlContainerComponent";
export declare class Button extends HtmlContainerComponent {
    constructor(vm?: ButtonViewModel);
    protected readonly vm: ButtonViewModel;
    readonly clicked: import("../Events").DefaultEventHandler<any>;
    changeTypeToSubmit(): void;
    enable(): void;
    disable(): void;
    private context;
    setContext(context: ContextualClass): void;
    private getContextCss;
    private isOutline;
    useOutlineStyle(): void;
}
