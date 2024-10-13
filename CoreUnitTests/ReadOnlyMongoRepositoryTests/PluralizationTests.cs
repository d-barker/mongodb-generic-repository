using System;
using Xunit;
using MongoDbGenericRepository.Utils;

namespace MongoDbGenericRepository.Tests.Utils
{
    public class PluralizationTest
    {
        [Fact]
        public void DefaultVocabulary_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var vocabulary = Vocabularies.Default;

            // Assert
            Assert.NotNull(vocabulary);
        }

        [Theory]
        [InlineData("cat", "cats")]
        [InlineData("dog", "dogs")]
        [InlineData("bus", "buses")]
        [InlineData("person", "people")]
        [InlineData("child", "children")]
        public void Pluralize_ShouldReturnCorrectPluralForm(string singular, string expectedPlural)
        {
            // Arrange
            var vocabulary = Vocabularies.Default;

            // Act
            var result = vocabulary.Pluralize(singular);

            // Assert
            Assert.Equal(expectedPlural, result);
        }

        [Theory]
        [InlineData("cats", "cat")]
        [InlineData("dogs", "dog")]
        [InlineData("buses", "bus")]
        [InlineData("people", "person")]
        [InlineData("children", "child")]
        public void Singularize_ShouldReturnCorrectSingularForm(string plural, string expectedSingular)
        {
            // Arrange
            var vocabulary = Vocabularies.Default;

            // Act
            var result = vocabulary.Singularize(plural);

            // Assert
            Assert.Equal(expectedSingular, result);
        }

        [Theory]
        [InlineData("fish")]
        [InlineData("sheep")]
        [InlineData("deer")]
        [InlineData("species")]
        [InlineData("series")]
        public void Pluralize_UncountableWords_ShouldReturnSameWord(string word)
        {
            // Arrange
            var vocabulary = Vocabularies.Default;

            // Act
            var result = vocabulary.Pluralize(word);

            // Assert
            Assert.Equal(word, result);
        }

        [Theory]
        [InlineData("fish")]
        [InlineData("sheep")]
        [InlineData("deer")]
        [InlineData("species")]
        [InlineData("series")]
        public void Singularize_UncountableWords_ShouldReturnSameWord(string word)
        {
            // Arrange
            var vocabulary = Vocabularies.Default;

            // Act
            var result = vocabulary.Singularize(word);

            // Assert
            Assert.Equal(word, result);
        }
    }
}