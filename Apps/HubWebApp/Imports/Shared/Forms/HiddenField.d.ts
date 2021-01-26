import { ConstraintCollection } from "./ConstraintCollection";
import { HiddenFieldViewModel } from "./HiddenFieldViewModel";
export declare class HiddenField<TValue> implements IField {
    private readonly vm;
    constructor(prefix: string, name: string, vm: HiddenFieldViewModel);
    private readonly name;
    getName(): string;
    private caption;
    setCaption(caption: string): void;
    getCaption(): string;
    getField(name: string): this;
    private value;
    setValue(value: TValue): void;
    getValue(): TValue;
    readonly constraints: ConstraintCollection;
    clearErrors(): void;
    validate(errors: IErrorList): void;
    import(values: Record<string, any>): void;
    export(values: Record<string, any>): void;
}
