export declare class ContextualClass {
    private readonly value;
    static readonly default: ContextualClass;
    static readonly success: ContextualClass;
    static readonly info: ContextualClass;
    static readonly warning: ContextualClass;
    static readonly danger: ContextualClass;
    static readonly primary: ContextualClass;
    static readonly secondary: ContextualClass;
    static readonly light: ContextualClass;
    static readonly dark: ContextualClass;
    private constructor();
    get isDefault(): boolean;
    get isSuccess(): boolean;
    get isInfo(): boolean;
    get isWarning(): boolean;
    get isDanger(): boolean;
    get isPrimary(): boolean;
    get isSecondary(): boolean;
    get isLight(): boolean;
    get isDark(): boolean;
    append(prefix: string): string;
    equals(other: ContextualClass | string): boolean;
    toString(): string;
}
