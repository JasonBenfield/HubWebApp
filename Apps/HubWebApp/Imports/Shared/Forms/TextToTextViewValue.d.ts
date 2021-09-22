import { TypedFieldViewValue } from "./TypedFieldViewValue";
export declare class TextToTextViewValue extends TypedFieldViewValue<string, string> {
    protected _fromView(value: string): string;
    protected _toView(value: string): string;
}
