function getPlural(number: number, one: string, few: string, many: string): string {
    const mod10 = number % 10;
    const mod100 = number % 100;
  
    if (mod10 === 1 && mod100 !== 11) return one;
    if (mod10 >= 2 && mod10 <= 4 && (mod100 < 10 || mod100 >= 20)) return few;
    return many;
}
  
export function getRelativeTime(publishedAt: Date | string): string {
    const now = new Date();
    const published = new Date(publishedAt);
    const diffMs = now.getTime() - published.getTime();

    const diffSeconds = Math.floor(diffMs / 1000);
    const diffMinutes = Math.floor(diffSeconds / 60);
    const diffHours = Math.floor(diffMinutes / 60);
    const diffDays = Math.floor(diffHours / 24);
    const diffWeeks = Math.floor(diffDays / 7);

    if (diffSeconds < 60) {
        return 'только что';
    } else if (diffMinutes < 60) {
        const word = getPlural(diffMinutes, 'минута', 'минуты', 'минут');
        return `${diffMinutes} ${word} назад`;
    } else if (diffHours < 24) {
        const word = getPlural(diffHours, 'час', 'часа', 'часов');
        return `${diffHours} ${word} назад`;
    } else if (diffDays < 7) {
        const word = getPlural(diffDays, 'день', 'дня', 'дней');
        return `${diffDays} ${word} назад`;
    } else {
        const options: Intl.DateTimeFormatOptions = {
        day: '2-digit',
        month: 'long',
        };

        if (published.getFullYear() !== now.getFullYear()) {
        options.year = 'numeric';
        }

        return published.toLocaleDateString('ru-RU', options);
    }
}

export function getDeclension(count: number): string {
    const mod10 = count % 10;
    const mod100 = count % 100;
  
    if (mod10 === 1 && mod100 !== 11) {
      return `${count} объявление`;
    } else if (mod10 >= 2 && mod10 <= 4 && (mod100 < 10 || mod100 >= 20)) {
      return `${count} объявления`;
    } else {
      return `${count} объявлений`;
    }
}

export function getNumberRuFormat(value: number): string {
    return new Intl.NumberFormat('ru-RU').format(value);
}

export function getFormattedFloat(value: number): string {
    return value.toFixed(1).replace('.', ',');
}

export function getFormattedEngineCapacity(value: number | null | undefined): string | null {
    if (value == null) {
        return null;
    }
    
    const liters = value / 1000;
    return liters.toFixed(1).replace('.', ',');
}
  
  