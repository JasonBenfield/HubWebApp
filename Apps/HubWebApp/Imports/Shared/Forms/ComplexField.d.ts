import { ColumnCss } from "../ColumnCss";
import { DateInputField } from "./DateInputField";
import { DropDownField } from "./DropDownField";
import { DropDownFieldViewModel } from "./DropDownFieldViewModel";
import { FieldCaption } from "./FieldCaption";
import { FieldValue } from "./FieldValue";
import { HiddenField } from "./HiddenField";
import { HiddenFieldViewModel } from "./HiddenFieldViewModel";
import { InputFieldViewModel } from "./InputFieldViewModel";
import { NumberInputField } from "./NumberInputField";
import { TextInputField } from "./TextInputField";
export declare class ComplexField implements IField {
    constructor(prefix: string, name: string, captionVM: IFieldCaptionViewModel, valueVM: IFieldValueViewModel);
    readonly caption: FieldCaption;
    readonly value: FieldValue;
    getName(): string;
    setCaption(caption: string): void;
    getCaption(): string;
    getField(name: string): any;
    setValue(value: any): void;
    getValue(): any;
    setColumns(captionColumns: ColumnCss, valueColumns: ColumnCss): void;
    private readonly fields;
    protected addHiddenTextField(name: string, vm: HiddenFieldViewModel): HiddenField<string>;
    protected addHiddenNumberField(name: string, vm: HiddenFieldViewModel): HiddenField<number>;
    protected addHiddenDateField(name: string, vm: HiddenFieldViewModel): HiddenField<Date>;
    protected addTextInputField(name: string, vm: InputFieldViewModel): TextInputField;
    protected addNumberInputField(name: string, vm: InputFieldViewModel): NumberInputField;
    protected addDateInputField(name: string, vm: InputFieldViewModel): DateInputField;
    protected addDropDownField<T>(name: string, vm: DropDownFieldViewModel): DropDownField<T>;
    addField<TField extends IField>(field: TField): TField;
    clearErrors(): void;
    validate(errors: IErrorList): void;
    import(values: Record<string, any>): void;
    export(values: Record<string, any>): void;
}
