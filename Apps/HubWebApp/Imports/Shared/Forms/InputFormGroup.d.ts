import { BlockViewModel } from "../Html/BlockViewModel";
import { Input } from "../Html/Input";
import { SimpleFieldFormGroup } from "./SimpleFieldFormGroup";
import { TypedFieldViewValue } from "./TypedFieldViewValue";
export declare abstract class InputFormGroup<TValue> extends SimpleFieldFormGroup<TValue> {
    private readonly viewValue;
    constructor(prefix: string, name: string, vm: BlockViewModel, viewValue: TypedFieldViewValue<string, TValue>);
    private onInputValueChanged;
    readonly input: Input;
    getValue(): TValue;
    setValue(value: TValue): void;
    setMaxLength(maxLength: number): void;
    protect(): void;
    setFocus(): void;
    blur(): void;
}
