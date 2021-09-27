import { HtmlComponent } from "./HtmlComponent";
import { ContextualClass } from "../ContextualClass";
import { InputViewModel } from "./InputViewModel";
export declare class Input extends HtmlComponent {
    constructor(vm?: InputViewModel);
    private readonly _changed;
    readonly changed: import("../Events").DefaultEventHandler<string>;
    private onValueChanged;
    protected readonly vm: InputViewModel;
    enable(): void;
    disable(): void;
    private value;
    getValue(): string;
    setValue(value: string): void;
    private border;
    setBorder(border: ContextualClass): void;
    private getBorderCss;
    setMaxLength(maxLength: number): void;
    setType(type: 'text' | 'hidden' | 'password' | 'date' | 'number'): void;
    setFocus(): void;
    blur(): void;
}
